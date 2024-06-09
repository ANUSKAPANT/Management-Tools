import React from 'react';
import { Navigate } from 'react-router-dom'

const ProtectedRoute = ({ children }) => {
  const isAuthenticated = () => {
    return localStorage.getItem('token') !== null && localStorage.getItem('token') !== 'undefined';
  };

  if(!isAuthenticated()) {
    return <Navigate to="/login" replace />
  }
  return children;

};
  
export default ProtectedRoute;