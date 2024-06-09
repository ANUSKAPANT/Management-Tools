import React from 'react';
import "./Layout.css";
import Sidebar from '../../components/Sidebar/Sidebar';
import NavigationBar from '../../components/Navbar/Navbar';

const Layout = ({ component: Component }) => {
  return (
    <div className='layout'>
      <div className="sidebar"><Sidebar/></div>
      <div className="content">
        <div className="navbar"><NavigationBar/></div>
        <div className="main-content">
          <Component />
        </div>
      </div>
    </div>
  );
}

export default Layout;
