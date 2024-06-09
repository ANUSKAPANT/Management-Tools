import './App.css';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Project from './views/Project/Project';
import Dashboard from './views/Dashboard/Dashboard';
import Login from './views/Login/Login';
import Layout from './views/Layout/Layout';
import Board from './views/Board/Board';
import 'bootstrap/dist/css/bootstrap.min.css';
import ProjectDetails from './views/Project/ProjectDetails';
import Task from './views/Task/Task';
import ProtectedRoute from "./ProtectedRoute";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/dashboard" element={<ProtectedRoute><Layout component={Dashboard} /></ProtectedRoute>} />
        <Route exact path="/projects" element={<ProtectedRoute><Layout component={Project} /></ProtectedRoute>} />
        <Route path="/projects/:id" element={<ProtectedRoute><Layout component={ProjectDetails} /></ProtectedRoute>} />
        <Route path="/mytasks" element={<ProtectedRoute><Layout component={Task} /></ProtectedRoute>} />
        <Route path="/boards" element={<ProtectedRoute><Layout component={Board} /></ProtectedRoute>} />
        <Route path="/" element={<Navigate replace to="/login" />} />
      </Routes>
    </Router>
  );
}

export default App;
