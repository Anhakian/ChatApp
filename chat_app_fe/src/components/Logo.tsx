import React from 'react';

interface LogoProps {
  title: string;
  subtitle: string;
}

const Logo: React.FC<LogoProps> = ({ title, subtitle }) => {
  return (
    <>
      <h1 className="text-4xl md:text-8xl font-bold text-accent text-center md:text-left mb-3">
        {title}
      </h1>
      <h3 className="text-2xl md:text-4xl text-accent text-center md:text-left mt-3">
        {subtitle}
      </h3>
    </>
  );
};

export default Logo;
