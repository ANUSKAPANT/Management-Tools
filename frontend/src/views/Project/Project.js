import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Table from "../../components/Table/Table";
import CustomModal from '../../components/CustomModal/CustomModal';
import ProjectForm from './ProjectForm';
import ConfirmDelete from '../../components/CustomModal/ConfirmDelete';

const Projects = () => {
  const [allProjects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal] = useState(false);
  const [editId, setEditId] = useState(null);
  const [deleteModal, setDeleteModal] = useState(false);
  const [deleteId, setDeleteId] = useState(null);

  const toggleDeleteModal = (e, projectId) => {
    e.stopPropagation();
    setDeleteId(projectId);
    setDeleteModal(!deleteModal);
  }

  const toggle = (e, projectId) => {
    e.stopPropagation();
    setEditId(projectId);
    setModal(!modal);
  }

  const handleCreate = (data) => {
    const newProjects = [...allProjects, data];
    setProjects(newProjects);
    setModal(false);
  }

  const handleUpdate = (data) => {
    const updatedProject = allProjects.filter(project => project.id !== data.id);
    setProjects([data, ...updatedProject]);
    setModal(false);
  }

  const handleDeleteClick = (e, projectId) => {
    if(projectId) {
      axios.delete(`http://localhost:5244/api/Projects/${projectId}`)
      .then(response => {
        setProjects(allProjects.filter(project => project.id !== projectId));
      })
      .catch(error => {
        console.error('Error deleting project:', error);
      });
    }
    setDeleteModal(false);
  };

  const columns = [
    {
      id: 'id',
      numeric: false,
      disablePadding: true,
      label: 'S.N',
    },
    {
      id: 'projectName',
      numeric: false,
      disablePadding: false,
      label: 'Name',
    },
    {
      id: 'projectDescription',
      numeric: false,
      disablePadding: false,
      label: 'Description',
    },
    {
      id: 'actions',
      numeric: false,
      disablePadding: false,
      label: 'Actions',
      delete: toggleDeleteModal,
      edit: toggle,
    },
  ];

  useEffect(() => {
    axios.get("http://localhost:5244/api/Projects")
      .then(response => {
        setProjects(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
        setLoading(false);
      });
  }, []);



  return (
    <div>
      {loading ? <div/> :  <Table data={allProjects} columns={columns} child={true} tableName="Projects" handleDeleteClick={handleDeleteClick} toggle={toggle}/>}
      <CustomModal component={ProjectForm} modal={modal} toggle={toggle} title="New Project" id={editId} handleUpdate={handleUpdate} handleCreate={handleCreate}/>
      <ConfirmDelete isOpen={deleteModal} onClose={toggleDeleteModal} onConfirm={handleDeleteClick} id={deleteId}/>
    </div>
  );
};

export default Projects;
