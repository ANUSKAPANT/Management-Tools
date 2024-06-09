import React, { useEffect, useState } from "react";
import { Row } from "reactstrap";
import { 
  DndContext, DragOverlay, KeyboardSensor, PointerSensor, useSensor, useSensors, closestCorners,
} from "@dnd-kit/core";
import { sortableKeyboardCoordinates } from "@dnd-kit/sortable";
import axios from 'axios';

import { Column } from "./Column/Column";
import "./KanbanBoard.css";
import Task from "../Kanban Board/Task/Task";

const KanbanBoard = ({ todos, setTodos, projectId, handleUpdateTodos }) => {
  const [tasks, setTasks] = useState({
    "Todo": [],
    "In Progress" : [],
    "In Review" : [],
    "Complete" : []
  });

  const statusMap = {
    0 : "Todo",
    1 : "In Progress",
    2 : "In Review",
    3 : "Complete"
  };

  useEffect(() => {
    const groupedTasks = {
      "Todo": [],
      "In Progress" : [],
      "In Review" : [],
      "Complete" : []
    };

    todos.forEach(todo => {
      const { status, ...data } = todo;
      const statusKey = statusMap[status];
      groupedTasks[statusKey].push(data);
    });

    setTasks(groupedTasks);
  }, [todos]);


  const updateStatus = (id, status, taskToMove) => {
    const statusKey = Object.entries(statusMap).find(([key, value]) => value === status)[0];
    const data = {...taskToMove, status: statusKey};
    axios.put(`http://localhost:5244/api/Todos/${id}`, data , {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    .then((response) => {
      console.log("Successfully Updated!");
      const newTodos = todos.map(todo => {
        if(todo.id === data.id) {
          return {...data, status: parseInt(data.status)};
        }
        return todo;
      });
      handleUpdateTodos(newTodos);
      setTodos(newTodos);
    })
    .catch((error) => console.error('Error:', error));
  }
  // console.log(todos, tasks);

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 8,
      },
    }),
    useSensor(KeyboardSensor, {
      coordinateGetter: sortableKeyboardCoordinates,
    })
  );

  const getTaskPos = (id, view = "Todo") => {
    const newPos = tasks[view].findIndex((task) => task.id === id);
    return newPos === -1 ? 0 : newPos; 
  };

  const [activeDragItem, setActiveDragItem] = useState(null);

  const handleDragStart = (event) => {
    const activeItem = event.active;
    let stats = activeItem.data.current.sortable.containerId;
    let data = tasks[stats].find(item => item.id === activeItem.id);
    setActiveDragItem(data);
  };


  const handleDragEnd = (event) => {
    setActiveDragItem(null);
    const { active, over } = event;
    const viewFrom = event.active.data.current.sortable.containerId;
    const viewTo = event.over?.data?.current?.sortable?.containerId || over.id;
    if (active.id === over.id) return;
    
    var taskToMove = null; 
    let newTasks = {...tasks};
    const originalPos = getTaskPos(active.id, viewFrom);
    const newPos = getTaskPos(over.id, viewTo);
    taskToMove = tasks[viewFrom].splice(originalPos, 1)[0];
    if(taskToMove) {
      if(tasks[viewTo].length === 0) newTasks[viewTo].push(taskToMove);
      else newTasks[viewTo].splice(newPos, 0, taskToMove);
    }

    setTasks(newTasks);

    if(viewFrom !== viewTo && taskToMove) {
      updateStatus(active.id, viewTo, taskToMove);
    }
  };

  const handleAddTask = (data) => {
    setTodos([...todos, data]);
  }

  const handleUpdateTask = (id, data) => {
    var i = todos.findIndex(todo => todo.id === id);
    if (i !== -1) {
      setTodos(prevTodos => {
        const updatedTodos = [...prevTodos];
        updatedTodos[i] = data;
        return updatedTodos;
      });
    }
    handleUpdateTodos(true);
  }

  return (
    <DndContext
      sensors={sensors}
      collisionDetection={closestCorners}
      onDragStart={handleDragStart}
      onDragEnd={handleDragEnd}
    >
      <Row className="board row">
        <Column id="Todo" propTasks={tasks["Todo"]} handleAddTask={handleAddTask} handleUpdateTask={handleUpdateTask} projectId={projectId}/>
        <Column id="In Progress" propTasks={tasks["In Progress"]} handleAddTask={handleAddTask} handleUpdateTask={handleUpdateTask} projectId={projectId}/>
        <Column id="In Review" propTasks={tasks["In Review"]} handleAddTask={handleAddTask} handleUpdateTask={handleUpdateTask} projectId={projectId}/>
        <Column id="Complete" propTasks={tasks["Complete"]} handleAddTask={handleAddTask} handleUpdateTask={handleUpdateTask} projectId={projectId}/>
      </Row>

      {activeDragItem && (
        <DragOverlay>
          <Task
            id={activeDragItem.id}
            title={activeDragItem.todoName}
            data={activeDragItem}
          />
        </DragOverlay>
      )}
    </DndContext>
  );
};

export default KanbanBoard;
