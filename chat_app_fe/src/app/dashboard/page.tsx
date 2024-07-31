'use client'

import React, { useState, useEffect } from 'react';
import AccountHandler from '@/components/AccountHandler';
import MessageSideBar from '@/components/chatInterface/MessageSideBar';
import { Conversation } from '@/types/conversation';
import ConversationView from '@/components/chatInterface/ConversationView';

const Dashboard: React.FC = () => {
    const [conversations, setConversations] = useState<Conversation[]>([]);
    const [isSideBarOpen, setIsSideBarOpen] = useState(true);
    const [selectedConversation, setSelectedConversation] = useState<Conversation | null>(null);

    useEffect(() => {
        // TODO: Fetch Conversations from database
        const fetchedConversations: Conversation[] = [
            { id: 1, name: 'Group 1', image: '/path/to/image1.jpg', lastMessage: 'Hello from Group 1, this is a long message that will be truncated' },
            { id: 2, name: 'Group 2', image: '/path/to/image2.jpg', lastMessage: 'Hello from Group 2' },
            { id: 3, name: 'Group 3', image: '/path/to/image3.jpg', lastMessage: 'Hello from Group 3' },
        ];
        setConversations(fetchedConversations);
    }, []);

    const toggleSideBar = () => {
        setIsSideBarOpen(!isSideBarOpen);
    };

    const handleConversationClick = (conversation: Conversation) => {
        setSelectedConversation(conversation);
        // On mobile, close the sidebar when a conversation is selected
        if (window.innerWidth < 768) {
            setIsSideBarOpen(false);
        }
    };

    return (
        <div className="min-h-screen bg-background flex flex-col">
            <AccountHandler />
            <main className="flex flex-grow overflow-hidden">
                <MessageSideBar 
                    conversations={conversations} 
                    isOpen={isSideBarOpen} 
                    toggleSidebar={toggleSideBar}
                    onConversationClick={handleConversationClick}
                />
                <div className="flex-grow overflow-hidden">
                    {selectedConversation ? (
                        <ConversationView conversation={selectedConversation} />
                    ) : (
                        <div className="flex items-center justify-center h-full">
                            <p className="text-gray-500">Select a conversation to start chatting</p>
                        </div>
                    )}
                </div>
            </main>
        </div>
    );
};

export default Dashboard;