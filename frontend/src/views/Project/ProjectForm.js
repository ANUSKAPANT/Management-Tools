import React, { useState, useEffect } from 'react';
import { FormGroup, Label, Input, Button, Form, Col, Card } from 'reactstrap';
import axios from 'axios';
import AsyncSelect from 'react-select/async';
import TaskForm from '../Task/TaskForm';
import CustomModal from '../../components/CustomModal/CustomModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faRemove } from '@fortawesome/free-solid-svg-icons';

const ProjectForm = ({id, handleUpdate, handleCreate}) => {
  const [selectedValue, setSelectedValue] = useState(null);
  const [modalTask, setModalTask] = useState(false);

  const [projectData, setProjectData] = useState({
    projectName: '',
    projectDescription: '',
    userIds: [],
    todos: [],
  });
  const toggleTask = () => setModalTask(!modalTask);

  useEffect(() => {
    if(id) {
      axios.get(`http://localhost:5244/api/Projects/${id}`)
      .then(response => {
        const userIds = response.data.users.map(u => u.id.toString())
        const todos = response.data.todos;
        setProjectData({...response.data, users: userIds, todos: todos});
        const users = response.data.users;
        setSelectedValue(users.map(user => ({label: `${user.username}`, value: user.id, data: user})));
      })
      .catch(error => {
        console.error('Error submitting form:', error);
      });
    }
  }, [])

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setProjectData({ ...projectData, [name]: value });
  };

  const handleRemoveTodo = (index) => {
    const updatedTodos = [...projectData.todos];
    updatedTodos.splice(index, 1);
    setProjectData({ ...projectData, todos: updatedTodos});
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if(id) {
      axios.put(`http://localhost:5244/api/Projects/${id}`, projectData)
      .then(response => {
        console.log('Form submitted successfully:', response.data);
        handleUpdate(response.data);
      })
      .catch(error => {
        console.error('Error submitting form:', error);
      });
    } else {
      axios.post('http://localhost:5244/api/Projects', projectData)
        .then(response => {
          console.log('Form submitted successfully:', response.data);
          handleCreate(response.data);
        })
        .catch(error => {
          console.error('Error submitting form:', error);
        });
    }
  };

  const handleChange = value => {
    const selectedUsers = Array.from(value, option => option.value);
    setProjectData({ ...projectData, userIds: selectedUsers });
    setSelectedValue(value);
  }

  const loadOptions = (inputValue, callback) => {
    loadData().then((results) => callback(results));
  };

  const loadData = () => axios.get("http://localhost:5244/api/accounts/users")
  .then((res) => res.data.map((x) => {
    return { value: x.id, label: `${x.username}`, data: x };
  })).catch(() => {});

  const customStyles = {
    control: (provided, state) => ({
      ...provided,
      // height: '60px',
      boxShadow: '0 2px 4px white'
    }),
  };

  const handleAddTodo = (data) => {
    const addTodos = projectData['todos'] || [];
    const newTodos = [...addTodos, data];
    setProjectData({ ...projectData, todos: newTodos });
    setModalTask(false);
  }

  return (
    <Form onSubmit={handleSubmit}>
      <FormGroup row>
        <Col sm={12}>
          <Label for="projectName">Project Name</Label>
          <Input required type="text" name="projectName" id="projectName" value={projectData.projectName} onChange={handleInputChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
          <Label for="projectDescription">Project Description</Label>
          <Input required type="textarea" name="projectDescription" id="projectDescription" value={projectData.projectDescription} onChange={handleInputChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
          <Label for="users">Add Members</Label>
          <AsyncSelect
            isMulti
            cacheOptions
            defaultOptions
            value={selectedValue}
            loadOptions={loadOptions}
            onChange={handleChange}
            styles={customStyles}
          />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
          <Label for="todos">Todos</Label>
          {projectData.todos.map((todo, index) => (
            <div className='add-todo'>
              <Card key={index} className='my-1 todo'>
                {todo.todoName}
                <FontAwesomeIcon icon={faRemove} className='icon' onClick={() => handleRemoveTodo(index)}/>
              </Card>
            </div>
          ))}
        </Col>
        <Col sm={12} className='my-1'>
          <Button color="success" onClick={toggleTask}>Add Todo</Button>
        </Col>
      </FormGroup>
      <FormGroup row className='mx-5 my-2'>
        <Button color="primary" type="submit">Submit</Button>
      </FormGroup>
      <CustomModal component={TaskForm} modal={modalTask} toggle={toggleTask} title="Add Todo" addUser={false} handleAdd={handleAddTodo} add={true} projectId={id}/>
    </Form>
  );
};

export default ProjectForm;
