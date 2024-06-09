import React, {useState} from 'react';
import './Navbar.css';
import {Nav, NavItem, Card, CardBody, Dropdown, 
  DropdownItem, DropdownToggle, DropdownMenu,
} from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faAngleDown } from '@fortawesome/free-solid-svg-icons';
import UserForm from '../../views/User/UserForm';
import CustomModal from '../CustomModal/CustomModal';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const NavigationBar = () => {

  const navigate = useNavigate();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [modal, setModal] = useState(false);

  const toggle = () => setModal(!modal);

  const handleLogout = () => {
    axios.post('http://localhost:5244/api/accounts/logout')
    .then(response => {
      localStorage.clear();
      navigate('/login');
    })
    .catch(error => {
      console.error('Error submitting form:', error);
    });
  }
 
  return(
    <>
    {/* <h3>Project Management Tool</h3> */}
    <Nav className='navigation'>
      <NavItem>
        <Dropdown isOpen={dropdownOpen} toggle={() => setDropdownOpen(!dropdownOpen)}>
          <DropdownToggle tag={Card} className="card-dropdown-toggle">
            <CardBody>
              <FontAwesomeIcon icon={faUser} className='px-2' />Anuska Pant <FontAwesomeIcon icon={faAngleDown} className='px-2' />
            </CardBody>
          </DropdownToggle>
          <DropdownMenu>
            <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
          </DropdownMenu>
        </Dropdown>
      </NavItem>
    </Nav>
    <CustomModal component={UserForm} modal={modal} toggle={toggle} title="Add User"/>
    </>
  );
};

export default NavigationBar;