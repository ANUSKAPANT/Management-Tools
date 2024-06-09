import { SortableContext, verticalListSortingStrategy } from "@dnd-kit/sortable";
import { useDroppable } from "@dnd-kit/core";
import {Col, CardHeader, CardFooter, Button} from 'reactstrap';
import { Task } from "../Task/Task";
import "./Column.css";
import TaskForm from "../../../views/Task/TaskForm";
import React, {useEffect, useState} from "react";
import CustomModal from "../../CustomModal/CustomModal";
import ConfirmDelete from "../../CustomModal/ConfirmDelete";
import axios from 'axios';

export const Column = (props) => {
  const {id, propTasks, handleAddTask, handleUpdateTask, projectId} = props;
  const { setNodeRef } = useDroppable({id});
  const [tasks, setTasks] = useState(propTasks);
  const [modalTask, setModalTask] = useState(false);
  const [modalDelete, setModalDelete] = useState(false);
  const [selectedId, setSelectedId] = useState(null);
  
  const toggleTask = () => {
    setModalTask(!modalTask);
  }

  useEffect(() => {
    setTasks(propTasks);
  }, [propTasks])

  const handleDelete = () => {
    if(selectedId) {
      axios.delete(`http://localhost:5244/api/Todos/${selectedId}`)
      .then(response => {
        const newTasks = tasks.filter(t => t.id !== selectedId);
        setTasks(newTasks);
      })
      .catch(error => {
        console.error('Error deleting task:', error);
      });
    }
    setModalDelete(false);
  }

  const handleDeleteClick = () => {
    setModalDelete(!modalDelete);
  }

  const handleUpdate = (data) => {
    setModalTask(false);
    handleUpdateTask(data.id, data);
  }

  return (
    <Col xs="12" md="4" lg="3" className="card column my-3">
      <CardHeader color="secondary">{id}</CardHeader>
      <SortableContext id={id} items={tasks} strategy={verticalListSortingStrategy}>
        <div ref={setNodeRef}>
          {tasks.map((task) => (
            <Task key={task.id} id={task.id} title={task.todoName} toggleTask={toggleTask} data={task} handleDeleteClick={handleDeleteClick} handleEditClick={toggleTask} setSelectedId={setSelectedId}/>
          ))}
        </div>
      </SortableContext>
      <CardFooter><Button onClick={toggleTask} className="add-btn my-5"> + Add New Card</Button></CardFooter>
      <CustomModal 
        id={selectedId}
        component={TaskForm}
        modal={modalTask}
        toggle={toggleTask}
        title="Add Todo"
        addUser={true}
        setModal={setModalTask}
        handleUpdate={handleUpdate}
        handleCreate={handleAddTask}
        projectId={projectId}
        defaultValue = {{status: id, projectId: projectId}}
      />
      <ConfirmDelete id={selectedId} isOpen={modalDelete} onClose={() => setModalDelete(!modalDelete)} onConfirm={handleDelete} />
    </Col>
  );
};