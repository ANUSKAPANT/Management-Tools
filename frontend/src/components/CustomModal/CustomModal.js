import React from 'react';
import {Modal, ModalBody, ModalHeader} from 'reactstrap';

const CustomModal = ({ component: Component, modal, setModal,
   toggle, title, id, handleCreate, handleUpdate, add, handleAdd, projectId, defaultValue, addUser}) => {
  return (
    <Modal isOpen={modal} toggle={toggle} >
      <ModalHeader toggle={toggle}>{title}</ModalHeader>
      <ModalBody>
        <Component id={id} setModal={setModal} handleCreate={handleCreate} handleUpdate={handleUpdate} handleAdd={handleAdd} add={add} projectId={projectId} defaultValue={defaultValue} addUser={addUser}/>
      </ModalBody>
    </Modal>
  )
}

export default CustomModal;