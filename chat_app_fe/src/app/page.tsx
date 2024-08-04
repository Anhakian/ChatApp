import Logo from '@/components/Logo'
import NavBar from '@/components/NavBar'
import React from 'react'

export default function Home() {
  return (
    <div>
        <NavBar />
        <main className="flex flex-col md:flex-row items-center justify-center min-h-screen bg-background">
            <div className="flex flex-col items-center justify-center space-y-4 md:mb-16">
                <Logo title='Welcome To ChatApp' 
                    subtitle='Dive into exciting conversations that spark your life' 
                />
            </div>
        </main>
    </div>
  )
}