import React, { useState, useEffect } from 'react';
import AsyncSelect from 'react-select/async';
import axios from 'axios';
import "./Board.css";
import KanbanBoard from '../../components/Kanban Board/KanbanBoard';

const Board = () => {
  const [todos, setTodos] = useState([]);
  const [selectedValue, setSelectedValue] = useState(null);
  const [defaultValue, setDefaultValue] = useState(null);
  const [stateChanged, setStateChanged] = useState(false);

  useEffect(() => {
    if(defaultValue != null) {
      setSelectedValue(defaultValue);
      setTodos(defaultValue.data.todos);
    }
  }, [defaultValue]);

  const handleChange = (value) => {
    setTodos(value.data.todos);
    setSelectedValue(value);
  }

  const handleUpdateTodos = (change= false, value=[]) => {
    if(!change) setTodos(value);
    setStateChanged(!stateChanged); 
  }

  const loadOptions = (inputValue, callback) => {
    loadData().then((results) => {
      if(results) {
        setDefaultValue(results[0]);
        return callback(results)
      }
    });
  };

  const loadData = () => axios.get("http://localhost:5244/api/Projects")
  .then((res) => res.data.map((x) => {
    return { value: x.id.toString(), label: x.projectName, data: x };
  })).catch(() => {});

  const customStyles = {
    control: (provided, state) => ({
      ...provided,
      backgroundColor:'#55898e',
      height: '60px',
      boxShadow: '0 2px 4px white'
    }),
    singleValue: (provided, state) => ({
      ...provided,
      color: 'white',
      fontWeight: 'bold',
    }),
  };

  return (
    <>
      <label className="custom-label">Select Project</label>
      <AsyncSelect
        key={stateChanged}
        className="custom-select"
        cacheOptions
        defaultOptions
        value={selectedValue}
        getOptionLabel={e => e.label}
        getOptionValue={e => e.value}
        loadOptions={loadOptions}
        onChange={handleChange}
        defaultValue={selectedValue}
        styles={customStyles}
      />
      {selectedValue && <KanbanBoard todos={todos} setTodos={setTodos} projectId={selectedValue.value} handleUpdateTodos={handleUpdateTodos}/>}
    </>
  ); 
};

export default Board;
