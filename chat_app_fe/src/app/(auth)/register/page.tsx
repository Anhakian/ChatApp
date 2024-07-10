'use client'

import React, { useState } from 'react';
import Link from 'next/link';
import AuthInput from '@/components/auth/AuthInput';
import Logo from '@/components/Logo';

export default function Register() {
  const [username, setUsername] = useState('');
  const [displayName, setDisplayName] = useState('');
  const [password, setPassword] = useState('');

  const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
    //TODO: Connect to the API and finish the function
  };
  
  return (
    <div>
      <main className="flex flex-col md:flex-row items-center justify-center min-h-screen bg-background">
        <div className="flex flex-col items-center justify-center space-y-4 md:mt-16 md:mb-16 md:mr-28">
          <Logo />
        </div>
        <div className="mt-8 md:mt-0 md:ml-36 md:w-auto">
          <div className="p-10 bg-white rounded-lg shadow-lg">
            <h2 className="text-4xl font-bold text-center text-accent mb-4">
              Register
            </h2>
            <form className="space-y-4" onSubmit={handleRegister}>
              <AuthInput
                label="Username"
                id="username"
                type="text"
                value={username}
                onChange={setUsername}
              />
              <AuthInput
                label="Display Name"
                id="displayName"
                type="text"
                value={displayName}
                onChange={setDisplayName}
              />
              <AuthInput
                label="Password"
                id="password"
                type="password"
                value={password}
                onChange={setPassword}
              />
              <div>
                <button type="submit" className="w-full bg-accent text-white px-7 py-3 rounded-lg hover:bg-accent-dark focus:outline-none focus:bg-accent-dark">Login</button>
              </div>
              <div className="text-lg font-medium text-center text-accent">
                Already have an account? <Link href="/login" className="underline hover:text-text cursor-pointer">Login</Link>
              </div>
            </form>
          </div>
        </div>
      </main>
    </div>
  );
}
