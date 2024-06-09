import React, { useState, useEffect } from 'react';
import { FormGroup, Label, Input, Button, Form, Col } from 'reactstrap';
import axios from 'axios';
import AsyncSelect from 'react-select/async';
import { useParams } from 'react-router-dom';

const TaskForm = ({id, setModal, handleCreate, handleUpdate, projectId, add, handleAdd, addUser, defaultValue}) => {
  const [selectedValue, setSelectedValue] = useState(null);
  const projId = useParams().id || projectId || null;
  const [taskData, setTaskData] = useState({
    todoName: '',
    todoDescription: '',
    userIds: [],
    status: 0,
    projectId: projId,
  });

  const mapStatus = {
   "Todo":  0,
   "In Progress":  1,
   "In Review":  2,
   "Complete": 3
  };

  useEffect(() => {
    if(id) {
      axios.get(`http://localhost:5244/api/Todos/${id}`)
      .then(response => {
        const userIds = response.data.users.map(u => u.id.toString())
        setTaskData({...response.data, userIds: userIds});
        const users = response.data.users;
        setSelectedValue(users.map(user => ({label: `${user.username}`, value: user.id.toString(), data: user})));
      })
      .catch(error => {
        console.error('Error submitting form:', error);
      });
    } else {
      if(defaultValue != null) {
        const { status, projectId } = defaultValue;
        setTaskData({...taskData, status: mapStatus[status], projectId: parseInt(projectId)});
      } else {
        setTaskData({...taskData});
      }
    }
  }, [])


  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setTaskData({ ...taskData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if(!id) {
      axios.post(`http://localhost:5244/api/Todos/${projId}`, taskData)
        .then(response => {
          console.log('Form submitted successfully:', response.data);
          handleCreate(response.data);
          setModal(false);
        })
        .catch(error => {
          console.error('Error submitting form:', error);
        });
    } else {
      axios.put(`http://localhost:5244/api/Todos/${id}`, taskData)
        .then(response => {
          console.log('Updated successfully:', response.data);
          handleUpdate(response.data);
          setModal(false);
        })
        .catch(error => {
          console.error('Error submitting form:', error);
        });
    }
  };

  const handleChange = value => {
    const selectedUsers = Array.from(value, option => option.value);
    setTaskData({ ...taskData, userIds: selectedUsers });
    setSelectedValue(value);
  }

  const loadOptions = (inputValue, callback) => {
    loadData().then((results) => callback(results));
  };

  const loadData = () => axios.get("http://localhost:5244/api/accounts/users")
  .then((res) => res.data.map((x) => {
    return { value: x.id, label: `${x.username}`, data: x };
  })).catch(() => {});


  return (
    <Form onSubmit={handleSubmit}>
      <FormGroup row>
        <Col sm={12}>
        <Label for="todoName">Todo Name</Label>
          <Input type="text" name="todoName" id="todoName" value={taskData.todoName} onChange={handleInputChange} required />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
        <Label for="todoDescription">Todo Description</Label>
          <Input type="textarea" name="todoDescription" id="todoDescription" value={taskData.todoDescription} onChange={handleInputChange} required />
        </Col>
      </FormGroup>
      {addUser ? 
          <FormGroup row>
          <Col sm={12}>
            <Label for="users">Add Members</Label>
            <AsyncSelect
              isMulti
              className="custom-select"
              cacheOptions
              defaultOptions
              value={selectedValue}
              loadOptions={loadOptions}
              onChange={handleChange}
            />
          </Col>
        </FormGroup> : <></>}
      <FormGroup row>
        <Col sm={12}>
        <Label for="status">Status</Label>
          <Input type="select" name="status" id="status" value={taskData.status} onChange={handleInputChange} required>
            <option value="0">Todo</option>
            <option value="1">In Progress</option>
            <option value="2">In Review</option>
            <option value="3">Complete</option>
          </Input>
        </Col>
      </FormGroup>
      {add ? (
        <FormGroup row>
          <Col sm={12}>
            <Button color="primary" type="button" onClick={() => handleAdd(taskData)}>AddTodo</Button>
          </Col>
        </FormGroup>
      ) : (
        <FormGroup row>
          <Col sm={12}>
            <Button color="primary" type="submit">Submit</Button>
          </Col>
        </FormGroup>
      )}
    </Form>
  );
};

export default TaskForm;
