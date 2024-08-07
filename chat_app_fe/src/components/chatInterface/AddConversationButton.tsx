import React from "react";
import { Plus } from "react-feather";

type Props = {
  onAddConversationClick: () => void;
  isOpen: boolean;
};

const AddConversationButton: React.FC<Props> = ({ onAddConversationClick, isOpen }) => {
  return (
    <button
      className="flex items-center justify-center bg-secondary text-white text-lg font-bold px-3 py-4 mb-5 rounded shadow-lg w-full"
      onClick={onAddConversationClick}
    >
      {isOpen ? (
        <>
          Add Conversation <Plus className="ml-2" size={30} />
        </>
      ) : (
        <Plus size={30} />
      )}
    </button>
  );
};

export default AddConversationButton;
