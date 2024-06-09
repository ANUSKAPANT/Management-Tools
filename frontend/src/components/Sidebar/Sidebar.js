import React, {useState} from 'react';
import "./Sidebar.css";
import { Nav, NavItem, NavLink } from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDashboard, faProjectDiagram,faBarsProgress} from '@fortawesome/free-solid-svg-icons';
import Logo from './logo.png';
import { useNavigate } from 'react-router-dom';

const Sidebar = () => {
  const navigate = useNavigate();
  const [activeLink, setActiveLink] = useState('dashboard');

  const handleNavLinkClick = (link) => {
    setActiveLink(link);
    navigate(`/${link}`);
  };

  return (
    <Nav vertical className='sidebar'>
      <NavItem className='logo'>
      <img src={Logo} alt="" className="mx-4 img-fluid rounded-circle" style={{ maxWidth: '7rem', maxHeight: '7rem' }} />
      </NavItem>
      <NavItem>
        <NavLink
          active={activeLink === 'dashboard'}
          onClick={() => handleNavLinkClick('dashboard')}
        >
          <FontAwesomeIcon icon={faDashboard} /> Dashboard
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink

          active={activeLink === 'projects'}
          onClick={() => handleNavLinkClick('projects')}
        >
          <FontAwesomeIcon icon={faProjectDiagram} /> Projects
        </NavLink>
      </NavItem>
      {/* <NavItem>
        <NavLink
          href="/mytasks"
          active={activeLink === 'tasks'}
          onClick={() => handleNavLinkClick('tasks')}
        >
          <FontAwesomeIcon icon={faTasks} /> My Tasks
        </NavLink>
      </NavItem> */}
      <NavItem>
        <NavLink
          active={activeLink === 'boards'}
          onClick={() => handleNavLinkClick('boards')}
        >
          <FontAwesomeIcon icon={faBarsProgress} /> Boards
        </NavLink>
      </NavItem>
    </Nav>
  );
};

export default Sidebar;
