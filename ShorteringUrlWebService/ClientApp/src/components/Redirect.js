import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class Redirect extends Component {
    componentDidMount() {
        this.getShortUrlsData();
    }

    getShortUrlsData() {
        fetch(`/api/ShorteringUrl/Get/${this.props.match.params.shortenUrlId}`)
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw response;
                }
            })
            .then(data => { window.location.href = data.url })
            .catch(error => { error.text().then(errorMessage => alert(errorMessage)) });
    }

    render() {
        return (
            <div>
                Loading...
            </div>
        );
    }
}

export default withRouter(Redirect)