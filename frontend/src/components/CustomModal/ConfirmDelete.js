import React from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from 'reactstrap';

const ConfirmDelete = ({ isOpen, onClose, onConfirm, id }) => {
  return (
    <Modal isOpen={isOpen} toggle={onClose}>
      <ModalHeader toggle={onClose}>Confirm Delete</ModalHeader>
      <ModalBody>
        <p>Are you sure you want to delete?</p>
      </ModalBody>
      <ModalFooter>
        <Button color="danger" onClick={(e) => onConfirm(e, id)}>Confirm</Button>
      </ModalFooter>
    </Modal>
  );
};

export default ConfirmDelete;
