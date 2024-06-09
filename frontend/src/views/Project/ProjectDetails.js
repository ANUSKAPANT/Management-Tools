import React, {useState, useEffect} from 'react';
import axios from 'axios';
import Table from "../../components/Table/Table";
import { useParams } from 'react-router-dom';
import KanbanBoard from '../../components/Kanban Board/KanbanBoard';
import { Button } from 'reactstrap';
import CustomModal from '../../components/CustomModal/CustomModal';
import TaskForm from '../Task/TaskForm';
import ConfirmDelete from '../../components/CustomModal/ConfirmDelete';
import "./Project.css";

const ProjectDetails = () => {
  const { id } = useParams();
  // const [project, setProject] = useState([]);
  const [todos, setTodos] = useState([]);
  const [boardTodos, setBoardTodos] = useState([]);
  const [loading, setLoading] = useState(true);
  const [boardView, setBoardView] = useState(false);
  const [modal, setModal] = useState(false);
  const [editId, setEditId] = useState(null);
  const [deleteModal, setDeleteModal] = useState(false);
  const [deleteId, setDeleteId] = useState(null);

  const toggleDeleteModal = (e, projectId) => {
    e.stopPropagation();
    setDeleteId(projectId);
    setDeleteModal(!deleteModal);
  }

  const toggle = (e, projectId) => {
    e.stopPropagation();
    setEditId(projectId);
    setModal(!modal);
  }

  const handleCreate = (data) => {
    setBoardTodos([...boardTodos, data]);
    const newTodos = {
      ...data,
      status: statusMap[data.status]
    };
    setTodos([...todos, newTodos]);
  }


  const handleUpdate = (updatedTodo) => {
    const updatedBoardTodos = boardTodos.filter(todo => todo.id !== updatedTodo.id);
    setBoardTodos([updatedTodo, ...updatedBoardTodos]);
  
    const updatedTodos = todos.filter(todo => todo.id !== updatedTodo.id);
     const newUpdatedTodo = {...updatedTodo, status: statusMap[updatedTodo.status]}
    setTodos([...updatedTodos, newUpdatedTodo]);
  }

  const handleDeleteClick = (e, todoId) => {
    if(todoId) {
      axios.delete(`http://localhost:5244/api/Todos/${todoId}`)
      .then(response => {
        const newBoardTodos = boardTodos.filter(item => item.id !== todoId);
        setBoardTodos(newBoardTodos);
        const newTodos = todos.filter(item => item.id !== todoId);
        setTodos(newTodos);
      })
      .catch(error => {
        console.error('Error deleting project:', error);
      });
    }
    setDeleteModal(false);
  };

  const columns = [
    {
      id: 'id',
      numeric: false,
      disablePadding: true,
      label: 'S.N',
    },
    {
      id: 'todoName',
      numeric: false,
      disablePadding: false,
      label: 'Task Name',
    },
    {
      id: 'todoDescription',
      numeric: false,
      disablePadding: false,
      label: 'Description',
    },
    {
      id: 'users',
      numeric: false,
      disablePadding: false,
      label: 'Assigned To',
      array: true,
    },
    {
      id: 'status',
      numeric: false,
      disablePadding: false,
      label: 'Status',
    },
    {
      id: 'actions',
      numeric: false,
      disablePadding: false,
      label: 'Actions',
      delete: toggleDeleteModal,
      edit: toggle,
    },
  ];

  const statusMap = {
    0 : "Todo",
    1 : "In Progress",
    2 : "In Review",
    3 : "Complete"
  };

  useEffect(() => {
    if(id != null) {
      axios.get(`http://localhost:5244/api/Projects/${id}`)
      .then(response => {
        const data = response.data;
        setBoardTodos(data.todos);
        const newTodos = data.todos.map(todo => ({
          ...todo,
          status: statusMap[todo.status],
        }));
        setTodos(newTodos);
        setLoading(false);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
        setLoading(false);
      });
    }
  }, []);

  const toggleView = (e) => {
    e.preventDefault();
    setBoardView(!boardView);
  }

  return (
    <div>
      <CustomModal 
        component={TaskForm} 
        modal={modal} 
        toggle={toggle} 
        setModal={setModal} 
        title="Add Todo" 
        addUser={true}
        id={editId} 
        handleCreate={handleCreate} 
        handleUpdate={handleUpdate}
      />
      <Button className="toggle-view" onClick={(e) => toggleView(e)}>{boardView ? "Table View" : "Board View"}</Button>
      {!loading && (boardView ? (<KanbanBoard todos={boardTodos} />) :  (<Table data={todos} columns={columns} tableName="Project Tasks" toggle={toggle}/>))}
      <ConfirmDelete isOpen={deleteModal} onClose={toggleDeleteModal} onConfirm={handleDeleteClick} id={deleteId}/>
    </div>
  ); 
};

export default ProjectDetails;


