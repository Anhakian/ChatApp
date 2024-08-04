"use client";

import React, { useState } from "react";
import { useRouter } from "next/navigation";
import Link from "next/link";
import AuthInput from "@/components/auth/AuthInput";
import Logo from "@/components/Logo";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const router = useRouter();

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const loginData = { username, password };

    try {
      const response = await fetch(`https://localhost:7252/api/auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(loginData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Login failed:", errorData.message);
        alert("Login failed: " + errorData.message);
        return;
      }

      const data = await response.json();
      console.log("Login successful:", data);

      router.push("/dashboard");
    } catch (error) {
      console.error("An error occurred:", error);
      alert("An error occurred: " + error);
    }
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
              Login
            </h2>
            <form className="space-y-4" onSubmit={handleLogin}>
              <AuthInput
                label="Username"
                id="username"
                type="text"
                value={username}
                onChange={(value) => setUsername(value)}
              />
              <AuthInput
                label="Password"
                id="password"
                type="password"
                value={password}
                onChange={(value) => setPassword(value)}
              />
              <div>
                <button
                  type="submit"
                  className="w-full bg-accent text-white px-7 py-3 rounded-lg hover:bg-accent-dark focus:outline-none focus:bg-accent-dark"
                >
                  Login
                </button>
              </div>
              <div className="text-lg font-medium text-center text-accent">
                <span>Don&apos;t have an account?</span>{" "}
                <Link
                  href="/register"
                  className="underline hover:text-text cursor-pointer"
                >
                  Sign Up
                </Link>
              </div>
            </form>
          </div>
        </div>
      </main>
    </div>
  );
}
