import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class List extends Component {

    constructor(props) {
        super(props);

        this.state = {
            loading: true
        };
    }

    componentDidMount() {
        this.getShortUrlsData();
    }

    async getShortUrlsData() {
        const response = await fetch('/api/ShorteringUrl/Get');
        const data = await response.json();

        this.setState({ list: data, loading: false });
    }

    renderShortUrls = () => {
        if (!this.state.list || !this.state.list.length) {
            return <div>List is empty</div>
        }

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Short Url</th>
                        <th>Full Url</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.list.map(item =>
                        <tr key={item.id}>
                            <td><Link to={`/${item.id}`}>{window.location.origin}/{item.id}</Link></td>
                            <td><a href={item.url}>{item.url}</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        const contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderShortUrls();

        return (
            <div>
                <h1 id="tabelLabel">Available Urls</h1>
                {contents}
            </div>
        );
    }
}
