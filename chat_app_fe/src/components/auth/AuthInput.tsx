import React, { useState } from 'react';

interface AuthInputProps {
  label: string;
  id: string;
  type: string;
  placeholder?: string;
  value: string;
  onChange: (value: string) => void;
}

const AuthInput: React.FC<AuthInputProps> = ({ label, id, type, placeholder, value, onChange }) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange(e.target.value);
  };

  return (
    <div>
      <label htmlFor={id} className="block text-lg font-medium text-accent">
        {label}
      </label>
      <input
        id={id}
        type={type}
        placeholder={placeholder}
        className="px-7 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-accent"
        value={value}
        onChange={handleChange}
      />
    </div>
  );
};

export default AuthInput;
