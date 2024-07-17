import React from 'react';
import { Conversation, MinimalConversation } from '@/types/conversation';

type Props = {
    conversation: Conversation | MinimalConversation;
};

const ConversationCard: React.FC<Props> = ({ conversation }) => {
    const isFullConversation = (conv: any): conv is Conversation => {
        return 'name' in conv && 'lastMessage' in conv;
    };

    return (
        <div className="flex items-center p-4 bg-gray-700 rounded-lg hover:bg-gray-600">
            <img src={conversation.image} alt={conversation.image} className="w-12 h-12 rounded-full mr-4" />
            <div className="w-2/3 flex-grow">
                {isFullConversation(conversation) && (
                    <>
                        <h3 className="text-lg font-semibold">{conversation.name}</h3>
                        <p className="text-sm text-gray-300 truncate">{conversation.lastMessage}</p>
                    </>
                )}
                {!isFullConversation(conversation) && (
                    <p className="text-lg font-semibold">Group Message</p>
                )}
            </div>
        </div>
    );
};

export default ConversationCard;
