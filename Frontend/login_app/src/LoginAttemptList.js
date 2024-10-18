import React, { useState, useEffect } from "react";
import "./LoginAttemptList.css";

const LoginAttempt = (props) => <li {...props}>{props.children}</li>;

const LoginAttemptList = (props) => {
	const [attempts, setAttempts] = useState([]);
	const [filterText, setFilterText] = useState('');

	const handleFilterTextChange = (e) => {
		const val = e.target.value;
		setFilterText(val);
	};

	useEffect(() => {
		let newAttempts = [...props.attempts];
		if (filterText.length > 0) {
			const re = new RegExp(filterText, "i");
			newAttempts = newAttempts.filter(function(a) {
				return re.test(a.login) || re.test(a.password);
			});
		}
		setAttempts(newAttempts);
	}, [props.attempts, filterText]);

	return (
		<div className="Attempt-List-Main">
			<p>Recent activity</p>
			<input type="input" placeholder="Filter..." 
				value={filterText} onChange={handleFilterTextChange} />
			<ul className="Attempt-List">
			{attempts.map(function(a) {
					return (
						<LoginAttempt>
							<span>Id='{a.login}'&nbsp;Pwd='{a.password}'</span>
						</LoginAttempt>
					);
				})}			
			</ul>
		</div>
	);
}

export default LoginAttemptList;