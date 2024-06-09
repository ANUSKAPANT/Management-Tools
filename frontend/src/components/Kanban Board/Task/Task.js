import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPen, faTrash, faUser } from '@fortawesome/free-solid-svg-icons';
import {Row, Col} from "reactstrap";

import "./Task.css";
import { Button } from "reactstrap";

export const Task = ({ id, title, data, view, handleEditClick, handleDeleteClick, setSelectedId}) => {
  
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({ id, view });

  const style = {
    transition,
    transform: CSS.Transform.toString(transform),
  };

  return (
    <div
      ref={setNodeRef}
      style={style}
      {...attributes}
      {...listeners}
      className="task"
    >
      <Row>
        <Col md="6" xs="9">{title}</Col>
        <Col md="4" xs="2" className=" d-flex ml-auto">
          <Button className="actions" onClick={() => { setSelectedId(id); handleEditClick()}}><FontAwesomeIcon icon={faPen} /></Button>
          <Button className="actions delete" onClick={() => { setSelectedId(id); handleDeleteClick()}}><FontAwesomeIcon icon={faTrash} /></Button>
        </Col>
        <hr />
        <div>{data.todoDescription}</div>
      </Row>

      <Row className="justify-content-end">
        <Col xs="3" md="6" lg="5">
          <FontAwesomeIcon icon={faUser} className='px-2 user-icon' />
          <div>{data.users.length > 0 ? data.users.map(user => <span key={user.id} className="badge rounded-pill bg-success text-light mx-1">{user.username}</span>) : 
          <span className="badge rounded-pill bg-secondary text-light mx-1">Not assigned</span>
          }</div>
        </Col>
      </Row>
    </div>
  );
};

export default Task;
