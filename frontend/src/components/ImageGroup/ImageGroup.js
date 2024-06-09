import React from 'react';
import './ImageGroup.css'; // Import CSS file for custom styling

const defaultImages = [
  {
    imageUrl: "https://www.w3schools.com/html/img_girl.jpg"
  },
  {
    imageUrl: "	https://www.w3schools.com/html/img_girl.jpg"
  },
  {
    imageUrl: "	https://www.w3schools.com/html/img_girl.jpg"
  },
  {
    imageUrl: "	https://www.w3schools.com/html/img_girl.jpg"
  },
  {
    imageUrl: "https://www.w3schools.com/html/img_girl.jpg"
  },
  {
    imageUrl: "https://www.w3schools.com/html/img_girl.jpg"
  }
]

const ImageGroup = (props) => {
  const users = props.users || defaultImages;
  return (
    <div className="justify-content-center img-group">
      {users && users.map(user => (
      <div className="circle-wrapper">
        <img src={user.imageUrl} alt="Circle 1" className="circle" />
      </div>
      ))}
    </div>
  );
};

export default ImageGroup;
