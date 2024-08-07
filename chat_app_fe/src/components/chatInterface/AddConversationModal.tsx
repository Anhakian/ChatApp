// components/AddConversationModal.tsx
import React, { useState } from 'react';

interface AddConversationModalProps {
  isOpen: boolean;
  onClose: () => void;
  onCreate: (userName2: string, conversationName: string) => void;
}

const AddConversationModal: React.FC<AddConversationModalProps> = ({ isOpen, onClose, onCreate }) => {
  const [otherUserName, setOtherUserName] = useState('');
  const [newConversationName, setNewConversationName] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onCreate(otherUserName, newConversationName);
    setNewConversationName('');
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg w-1/3">
        <h2 className="text-xl font-bold mb-4">New Conversation</h2>
        <form onSubmit={handleSubmit}>
          <label className="block mb-2">
            Add User:
            <input
              type="text"
              className="block w-full border border-gray-300 p-2 rounded"
              value={otherUserName}
              placeholder='Please enter the Username of that User'
              onChange={(e) => setOtherUserName(e.target.value)}
              required
            />
          </label>
          <label className="block mb-2">
            Conversation Name
            <input
              type="text"
              className="block w-full border border-gray-300 p-2 rounded"
              value={newConversationName}
              placeholder='Enter Conversation Name'
              onChange={(e) => setNewConversationName(e.target.value)}
            />
          </label>
          <button
            type="submit"
            className="bg-blue-500 text-white px-4 py-2 rounded"
          >
            Create
          </button>
          <button
            type="button"
            className="ml-2 bg-gray-300 text-gray-700 px-4 py-2 rounded"
            onClick={onClose}
          >
            Cancel
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddConversationModal;
