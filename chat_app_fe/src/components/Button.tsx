import React from 'react';

interface ButtonProps {
  type: 'button' | 'submit' | 'reset';
  name: string;
}

const Button: React.FC<ButtonProps> = ({ type, name }) => {
  return (
    <>
      <button type={type} className="w-full bg-accent text-white text-xl px-7 py-3 rounded-lg hover:bg-primary hover:text-accent">
        {name}
      </button>
    </>
  );
};

export default Button