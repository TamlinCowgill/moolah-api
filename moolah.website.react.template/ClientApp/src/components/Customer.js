import React, { Component } from 'react';

export class Customer extends Component {
    static displayName = Customer.name;

    constructor(props) {
        super(props);
        this.state = { customers: [], loading: true };
    }

    componentDidMount() {
        this.populateCustomerData();
    }

    static renderCustomerTable(customers) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Date Created</th>
                        <th>Date Updated</th>
                        <th>Age</th>
                        <th>Date Of Birth</th>
                        <th>Tags</th>
                    </tr>
                </thead>
                <tbody>
                    {customers.map(c =>
                        <tr key={c.customerId}>
                            <td>{c.customerId}</td>
                            <td>{c.name}</td>
                            <td>{c.dateCreated}</td>
                            <td>{c.dateUpdated}</td>
                            <td>{c.age}</td>
                            <td>{c.dateOfBirth}</td>
                            <td>{c.tags}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Customer.renderCustomerTable(this.state.customers);

        return (
            <div>
                <h1 id="tabelLabel" >Customer</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateCustomerData() {
        const response = await fetch('customers');
        const data = await response.json();
        this.setState({ customers: data, loading: false });
    }
}
