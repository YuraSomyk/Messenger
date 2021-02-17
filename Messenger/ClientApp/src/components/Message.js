import React, { Component } from 'react';

export class Message extends Component {

    static displayName = Message.name;

    constructor(props) {
        super(props);
        this.state = { messages: [], loading: true };
    }

    componentDidMount() {
        this.MessengerData();
    }

    static renderForecastsTable(messages) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Message</th>
                        <th>User</th>
                    </tr>
                </thead>
                <tbody>
                    {messages.map(message =>
                        <tr key={message.data}>
                            <td>{message.id}</td>
                            <td>{message.messageString}</td>
                            <td>{message.user.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Message.renderForecastsTable(this.state.messages);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async MessengerData() {
        const response = await fetch('messanger');
        const data = await response.json();
        this.setState({ messages: data, loading: false });
    }
}