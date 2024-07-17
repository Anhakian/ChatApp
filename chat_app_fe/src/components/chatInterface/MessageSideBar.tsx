import React from 'react';
import ConversationCard from './ConversationCard';
import { Conversation, MinimalConversation } from '@/types/conversation';
import { Menu, X } from 'react-feather';

type Props = {
    conversations: (Conversation | MinimalConversation)[];
    isOpen: boolean;
    toggleSidebar: () => void;
};

const MessageSideBar: React.FC<Props> = ({ conversations, isOpen, toggleSidebar }) => {
    return (
        <div className={`w-${isOpen ? '1/5' : '1/6'} h-screen bg-gray-800 text-white p-4 overflow-y-auto flex-shrink-0`}>
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-xl font-semibold">Conversations</h2>
                <button onClick={toggleSidebar} className="text-gray-300 hover:text-white focus:outline-none">
                    <Menu size={24} />
                </button>
            </div>
            <div className="space-y-2">
                {conversations.map(conversation => (
                    isOpen ? (
                        <ConversationCard
                            key={conversation.id}
                            conversation={conversation as Conversation}
                        />
                    ) : (
                        <div key={conversation.id} className="flex items-center p-2 bg-gray-700 rounded-lg hover:bg-gray-600">
                            <img src={conversation.image} alt={conversation.name} className="w-10 h-10 rounded-full mr-2" />
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};

export default MessageSideBar;
