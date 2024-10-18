import React, { useRef } from "react";
import './LoginForm.css';

const LoginForm = (props) => {
	const loginInputRef = useRef();
	const passwordInputRef = useRef();

	const handleSubmit = (event) => {
		event.preventDefault();

		props.onSubmit({
			login: loginInputRef.current.value,
			password: passwordInputRef.current.value,
		});
	}

	return (
		<form className="form">
			<h1>Login</h1>
			<label htmlFor="name">Name</label>
			<input type="text" id="name" ref={loginInputRef} />
			<label htmlFor="password">Password</label>
			<input type="password" id="password" ref={passwordInputRef} />
			<button type="submit" onClick={handleSubmit}>Continue</button>
		</form>
	)
}

export default LoginForm;