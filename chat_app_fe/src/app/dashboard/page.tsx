'use client'

import React, { useState, useEffect } from 'react';
import AccountHandler from '@/components/AccountHandler';
import MessageSideBar from '@/components/chatInterface/MessageSideBar';
import { Conversation } from '@/types/conversation';

const Dashboard: React.FC = () => {
    const [conversations, setConversations] = useState<Conversation[]>([]);
    const [isSideBarOpen, setIsSideBarOpen] = useState(true);

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
      console.log('Click')
      console.log(isSideBarOpen)
    };

    return (
        <div className="min-h-screen bg-background">
            <AccountHandler />
            <main className="flex md:flex-row items-stretch"> 
                <MessageSideBar conversations={conversations} isOpen={isSideBarOpen} toggleSidebar={toggleSideBar} />
                <div className="flex-grow p-4">
                    {/* Main content goes here */}
                </div>
            </main>
        </div>
    );
};

export default Dashboard;
