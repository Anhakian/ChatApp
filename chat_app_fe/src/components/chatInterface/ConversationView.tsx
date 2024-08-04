import React from 'react';
import { Conversation } from '@/types/conversation';

type Props = {
    conversation: Conversation;
};

const ConversationView: React.FC<Props> = ({ conversation }) => {
    return (
        <div className="h-full p-4">
            <h2 className="text-2xl font-bold mb-4">{conversation.name}</h2>
            <div className="bg-white rounded-lg p-4 shadow">
                <p>{conversation.lastMessage}</p>
                {/* Add more conversation content here */}
            </div>
        </div>
    );
};

export default ConversationView;