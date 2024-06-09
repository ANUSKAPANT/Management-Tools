import React, { useState, useEffect } from 'react';
import { Card, Progress, CardBody, CardHeader, Row, Col, Badge } from 'reactstrap';
import { CircularProgressbar } from 'react-circular-progressbar';
import 'react-circular-progressbar/dist/styles.css';
import axios from 'axios';
import "./Dashboard.css";

const Dashboard = () => {
  const [allProjects, setProjects] = useState([]);
  const [allStats, setAllStats] = useState(null);
  const [incompleteTasks, setIncompleteTasks] = useState([]);
  const [loading, setLoading] = useState(true);

  const statusMap = {
    0 : "Todo",
    1 : "In Progress",
    2 : "In Review",
    3 : "Complete"
  };

  useEffect(() => {
    axios.get("http://localhost:5244/api/Projects")
    .then(response => {
      const projects = calculateProgress(response.data);
      setProjects(projects);
      setLoading(false);
    })
    .catch(error => {
      console.error('Error fetching data:', error);
      setLoading(false);
    });
  }, []);

  const calculateProgress = (projects) => {
    const stats = {"completed": 0, "incomplete": 0, "total": 0};
    var allTodos = [];
    const newProjects = projects.map(project => {
      const todos = project.todos;
      allTodos = [...allTodos, ...todos];
      const total = todos.length;
      const sum = todos.reduce((accumulator, todo) => accumulator + todo.status, 0);
      todos.forEach((todo) => {
        if(todo.status === 3) {
          stats["completed"] += 1;
        } else {
          stats["incomplete"] +=1;
        }
        stats["total"] += 1;
      });
      const progress = Math.trunc(sum * 100 / (total * 3));
      return {...project, progress: progress}
    })
    const items = allTodos.filter((item) => item.status < 3);
    setIncompleteTasks(items);
    setAllStats({...stats, incompletePercentage: Math.trunc(stats["incomplete"] * 100/stats["total"]), completePercentage: Math.trunc(stats["completed"] * 100/stats["total"])});
    return newProjects;
  }

  return (
    <>
      <Row className='progress-row'>
        <Col>
          <Card>
            <CardHeader>Completed Tasks</CardHeader>
            <CardBody className='completed'>
              <CircularProgressbar
                value={allStats && allStats['completePercentage']}
                text={allStats && `${allStats['completePercentage']}%`}
              />
            </CardBody>
            <div className='count'>
              <p className='bold'>{allStats &&  allStats['completed']}</p>
              <hr className='separate'/>
              <p className='text-muted'>Task Count</p>
            </div>
          </Card>
        </Col>
        <Col>
          <Card>
            <CardHeader>Incomplete Tasks</CardHeader>
            <CardBody className='incompleted'>
              <CircularProgressbar
                value={allStats && allStats['incompletePercentage']}
                text={allStats && `${allStats['incompletePercentage']}%`}
              />
            </CardBody>
            <div className='count'>
              <p className='bold'>{allStats &&  allStats['incomplete']}</p>
              <hr className='separate'/>
              <p className='text-muted'>Task Count</p>
            </div>
          </Card>
        </Col>
        <Col>
          <Card>
            <CardHeader>Total Tasks</CardHeader>
            <CardBody className='total'>
              <CircularProgressbar
                value={allStats && allStats['completePercentage']}
                text={allStats && allStats['total']}
              />
            </CardBody>
            <div className='count'>
              <p className='bold'>{allStats &&  allStats['total']}</p>
              <hr className='separate'/>
              <p className='text-muted'>Task Count</p>
            </div>
          </Card>
        </Col>
      </Row>
      <Row className="project-row">
        <Card className='col'>
          <CardHeader>Projects</CardHeader>
          <CardBody>
            {allProjects.map((project, index) => (
              <Row key={index}>
                <Col className="proj-name">{project.projectName}</Col>
                <Col className='status'><Progress value={project.progress} color={project.progress < 50 ? "warning": "success"}>{project.progress}%</Progress></Col>
                {index < allProjects.length - 1 && <hr className="separator" />}
              </Row>
            ))}
          </CardBody>
        </Card>
        <Card className='col'>
          <CardHeader>Incomplete Tasks</CardHeader>
          <CardBody>
            {incompleteTasks.map((itask, index) => (
              <Row key={index}>
                <Col className="proj-name">{itask.todoName}</Col>
                <Col className='status'>
                  <Badge
                    color={itask.status === 0 ? 'secondary' : itask.status === 1 ? 'primary' : 'success'}
                    pill
                  >
                    {itask && statusMap[itask.status]}
                  </Badge>
                </Col>
                {index < incompleteTasks.length - 1 && <hr className="separator" />}
              </Row>
            ))}
          </CardBody>
        </Card>
      </Row>
      {/* <Row className='project-row my-2'>
        <Card className='col'>
          <CardHeader>Projects</CardHeader>
          <CardBody>
            {allProjects.map((project, index) => (
              <Row key={index}>
                <Col className="proj-name">{project.projectName}</Col>
                <Col className='status'><Progress value={project.progress} color={project.progress < 50 ? "warning": "success"}>{project.progress}%</Progress></Col>
                {index < allProjects.length - 1 && <hr className="separator" />}
              </Row>
            ))}
          </CardBody>
        </Card>
        <Card className='col'>
          <CardHeader>Projects</CardHeader>
          <CardBody>
            {allProjects.map((project, index) => (
              <Row key={index}>
                <Col className="proj-name">{project.projectName}</Col>
                <Col className='status'><Progress value={project.progress} color={project.progress < 50 ? "warning": "success"}>{project.progress}%</Progress></Col>
                {index < allProjects.length - 1 && <hr className="separator" />}
              </Row>
            ))}
          </CardBody>
        </Card>
        <Card className='col'>
          <CardHeader>Projects</CardHeader>
          <CardBody>
            {allProjects.map((project, index) => (
              <Row key={index}>
                <Col className="proj-name">{project.projectName}</Col>
                <Col className='status'><Progress value={project.progress} color={project.progress < 50 ? "warning": "success"}>{project.progress}%</Progress></Col>
                {index < allProjects.length - 1 && <hr className="separator" />}
              </Row>
            ))}
          </CardBody>
        </Card>
      </Row> */}
    </>
  ); 
};

export default Dashboard;
