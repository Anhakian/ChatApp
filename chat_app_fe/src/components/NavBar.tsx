import Link from 'next/link';
import React from 'react';

const NavBar = () => {
    return (
        <nav className="bg-secondary p-2 md:p-4">
            <div className="max-w-8xl mx-auto">
                <div className="flex justify-between items-center">
                    <div>
                        <Link href="/" className="text-white text-3xl md:text-4xl lg:text-4xl ml-5 center font-bold">Chat App</Link>
                    </div>
                    <div className="flex space-x-1 md:space-x-2 lg:space-x-3 mr-5">
                        <Link href="/" className="text-white text-lg md:text-xl lg:text-2xl hover:text-text px-2 md:px-3 py-1 md:py-2 rounded">
                            Home
                        </Link>
                        <Link href="/register" className="text-white text-lg md:text-xl lg:text-2xl hover:bg-primary hover:text-accent px-2 md:px-3 py-1 md:py-2 rounded">
                            Register
                        </Link>
                        <Link href="/login" className="text-white text-lg md:text-xl lg:text-2xl hover:bg-primary hover:text-accent px-2 md:px-3 py-1 md:py-2 rounded">
                            Login
                        </Link>
                    </div>
                </div>
            </div>
        </nav>
    );
}

export default NavBar;
