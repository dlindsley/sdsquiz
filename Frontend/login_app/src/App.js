import React, { useState } from "react";
import './App.css';
import LoginForm from './LoginForm';
import LoginAttemptList from './LoginAttemptList';

const App = () => {
  const [loginAttempts, setLoginAttempts] = useState([]);

  const handleLoginSubmit = ({ login, password }) => {
    console.log({ login, password });

    const newLoginAttempts = [...loginAttempts, { login, password }];
    setLoginAttempts(newLoginAttempts);
  }

  return (
    <div className="App">
      <LoginForm onSubmit={handleLoginSubmit} />
      <LoginAttemptList attempts={loginAttempts} />
    </div>
  );
};

export default App;
