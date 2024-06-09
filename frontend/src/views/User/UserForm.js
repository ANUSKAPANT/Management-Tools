import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom'; // Assuming you're using React Router
import { FormGroup, Label, Input, Button, Form, Col } from 'reactstrap';
import axios from 'axios';

const UserForm = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    role: '',
    imageUrl: '',
    todos: [],
    projects: [],
  });

  const { id } = useParams();

  useEffect(() => {
    if (id) {
      // Fetch user data if id is present
      axios.get(`http://localhost:5244/api/User/${id}`)
        .then(response => {
          const userData = response.data;
          setFormData(userData);
        })
        .catch(error => {
          console.error('Error fetching user data:', error);
        });
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    axios.post('http://localhost:5244/api/User', formData)
      .then(response => {
        console.log('Form submitted successfully:', response.data);
      })
      .catch(error => {
        console.error('Error submitting form:', error);
      });
  };

  return (
    <Form onSubmit={handleSubmit}>
      <FormGroup row>
        <Col  sm={6}>
          <Label for="firstName">First Name</Label>
          <Input  required  type="text" name="firstName" id="firstName" value={formData.firstName} onChange={handleChange} />
        </Col>
        <Col sm={6}>
          <Label for="lastName">Last Name</Label>
          <Input  required  type="text" name="lastName" id="lastName" value={formData.lastName} onChange={handleChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
        <Label for="email" sm={2}>Email</Label>
          <Input  required  type="email" name="email" id="email" value={formData.email} onChange={handleChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
          <Label for="password" sm={2}>Password</Label>
          <Input required  type="password" name="password" id="password" value={formData.password} onChange={handleChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
          <Label for="imageUrl">ImageUrl</Label>
          <Input required type="url" name="imageUr" id="imageUr" defaultValue="https://www.w3schools.com/html/img_girl.jpg" value={formData.imageUrl} onChange={handleChange} />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col sm={12}>
        <Label for="role">Role</Label>
          <Input  required type="select" name="role" id="role" value={formData.role} onChange={handleChange}>
            <option value="">Select Role</option>
            <option value="admin">Admin</option>
            <option value="manager">Manager</option>
            <option value="employee">Employee</option>
          </Input>
        </Col>
      </FormGroup>
      <FormGroup row className='mx-5 my-2'>
        <Button color="primary" type="submit">Submit</Button>
      </FormGroup>
    </Form>
  );
};

export default UserForm;
