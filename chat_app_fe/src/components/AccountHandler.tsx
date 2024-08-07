"use client";

import React, { useState } from "react";
import Link from "next/link";
import { Settings } from "react-feather";

const AccountHandler: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);

  const toggleDropdown = () => {
    setIsOpen(!isOpen);
  };

  return (
    <nav className="bg-secondary p-2 md:p-4">
      <div className="max-w-8xl mx-auto">
        <div className="flex justify-between items-center">
          <div>
            <Link
              href="/"
              className="text-white text-3xl md:text-4xl lg:text-4xl ml-5 center font-bold"
            >
              ChatApp
            </Link>
          </div>
          <div className="relative flex items-center">
            <Link
              href="/"
              className="flex items-center font-bold text-lg text-white mr-3"
            >
              {localStorage.getItem("displayName")}
            </Link>
            <button
              onClick={toggleDropdown}
              className="text-white text-lg md:text-xl lg:text-2xl hover:text-text px-2 md:px-3 py-1 md:py-2 rounded flex items-center"
            >
              <Settings size={24} />
            </button>
            {isOpen && (
              <div className="absolute right-0 mt-2 w-48 bg-white border border-gray-200 rounded-lg shadow-lg">
                <Link
                  href="/"
                  className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                >
                  Option 1
                </Link>
                <Link
                  href="/"
                  className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                >
                  Option 2
                </Link>
                <Link
                  href="/"
                  className="block px-4 py-2 text-gray-800 hover:bg-gray-200"
                >
                  Option 3
                </Link>
              </div>
            )}
          </div>
        </div>
      </div>
    </nav>
  );
};

export default AccountHandler;