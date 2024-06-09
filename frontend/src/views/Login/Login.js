import React, { useState } from "react";
import "./Login.css";
// reactstrap components
import { Button, Card, CardBody, CardTitle, FormGroup, Form, Input, Col, Label
} from "reactstrap";
import axios from "axios";
import Snackbar from '@mui/material/Snackbar';
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarMsg, setSnackbarMsg] = useState("");
  const navigate = useNavigate();

  const notify = (message) => {
    setOpenSnackbar(true);
    setSnackbarMsg(message);
  };

  const handleSubmit = (event, guest=false) => {
    event.preventDefault();
    axios({
      method: "post",
      url: "http://localhost:5244/api/accounts/login",
      data: {
        username: guest ? 'Anuska' : userName,
        password: guest ? 'Test123@' : password
      },
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((response) => {
        const { token } = response.data;
        localStorage.setItem("token", token);
        navigate('/boards');
      })
      .catch((res) => {
        notify(res.response.data);
      });
  };

  const handleSnackbarClose = () => {
    setOpenSnackbar(false);
  };

  return (
    <>
      <Snackbar
        open={openSnackbar}
        autoHideDuration={3000}
        onClose={handleSnackbarClose}
        message={snackbarMsg}
      />
      <div className="bg-gradient-info">
        <div className="container d-flex flex-column min-vh-100 justify-content-center align-items-center">
          <Col lg="4" md="6" className="mx-auto my-auto">
            <Card className="bg-white login-card">
              <CardTitle tag="h1">Log In</CardTitle>
              <CardBody>
                <Form role="form" onSubmit={handleSubmit}>
                  <FormGroup>
                    <Label for="userName">Username</Label>
                    <Input
                      type="name"
                      name="name"
                      id="username"
                      onChange={(e) => setUserName(e.target.value)}
                    />
                  </FormGroup>
                  <FormGroup>
                    <Label for="userPassword">Password</Label>
                    <Input
                      type="password"
                      name="password"
                      id="userPassword"
                      onChange={(e) => setPassword(e.target.value)}
                    />
                  </FormGroup>
                  <Button
                    block
                    className="mt-3"
                    color="success"
                    onClick={handleSubmit}
                    size="lg"
                    type="submit"
                    id="login_button"
                  >
                    Submit
                  </Button>
                  <Button
                    block
                    className="mt-3"
                    color="primary"
                    onClick={(e) => handleSubmit(e, true)}
                    size="lg"
                    type="submit"
                    id="guest_login"
                  >
                    Guest Login
                  </Button>
                </Form>
              </CardBody>
            </Card>
          </Col>
        </div>
      </div>
    </>
  );
}
