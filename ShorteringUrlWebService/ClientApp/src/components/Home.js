import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class Home extends Component {
    constructor(props) {
        super(props);

        this.state = {};
    }

    handleSubmit = () => {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state.url)
        };
        fetch('/api/ShorteringUrl/Post', requestOptions)
            .then(response => {
                if (response.ok) {
                    return response.json()
                } else {
                    throw response;
                }
            })
            .then(data => { this.setState({ shortUrlModel: data }) })
            .catch(error => { error.text().then(errorMessage => alert(errorMessage)) });
    }

    handleChange = ({ target }) => {
        this.setState({ url: target.value });
    }

    render() {
        return (
            <div>
                <input type="text" placeholder="Enter Url" onChange={this.handleChange} />
                <button onClick={this.handleSubmit}>Submit</button>
                {
                    this.state.shortUrlModel && <div><Link to={`/${this.state.shortUrlModel.id}`}>{window.location.href}{this.state.shortUrlModel.id}</Link></div>
                }
            </div>
        );
    }
}
