export default function Home() {
  return (
    <div>
      <main className="flex flex-col md:flex-row items-center justify-center min-h-screen bg-background">
        <div className="flex flex-col items-center justify-center space-y-4 md:mt-16 md:mb-16 md:mr-20">
          <h1 className="text-5xl md:text-8xl font-bold text-text text-center md:text-left">
            ChatApp
          </h1>
          <h3 className="text-xl md:text-2xl text-text text-center md:text-left">
            A Chatting Platform
          </h3>
        </div>
        <div className="mt-8 md:mt-0 md:ml-20 md:w-auto">
          <div className="p-10 bg-primary rounded-lg shadow-lg border border-gray-300">
            <h2 className="text-4xl font-bold text-center text-accent mb-4">
              Login
            </h2>
            <form className="space-y-4">
              <div>
                <label htmlFor="username" className="block text-lg font-medium text-accent">Username</label>
                <input id="username" type="text" className="px-7 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-accent" />
              </div>
              <div>
                <label htmlFor="password" className="block text-lg font-medium text-accent">Password</label>
                <input id="password" type="password" className="w-full px-7 py-3 border border-gray-300 rounded-lg focus:outline-none focus:border-accent" />
              </div>
              <div>
                <button type="submit" className="w-full bg-accent text-color px-7 py-3 rounded-lg hover:bg-accent-dark focus:outline-none focus:bg-accent-dark">Login</button>
              </div>
              <div className="text-lg font-medium text-center text-accent">
                Don't have an account? Sign Up
              </div>
            </form>
          </div>
        </div>
      </main>
    </div>
  );
}
