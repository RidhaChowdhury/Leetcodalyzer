import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        // The variables this component needs
        this.state = { problems: [], loading: true };
    }

    componentDidMount() {
        this.getProblemSet();
    }

    static renderProblemTable(problems) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>Difficulty</th>
                        <th>Category</th>
                    </tr>
                </thead>
                <tbody>
                    {problems.map(problems =>
                        <tr key={problems.number}>
                            <td>{problems.number}</td>
                            <td>{problems.name}</td>
                            <td>{problems.difficulty}</td>
                            <td>{problems.category}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {    
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderProblemTable(this.state.problems);

        return (
            <div>
                <h1 id="tabelLabel" >Problems</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async getProblemSet() {
        console.log("Fetching leetcodes")
        const response = await fetch('leetcodeproblem');
        const data = await response.json();
        console.log(JSON.stringify(data));
        this.setState({ problems: data, loading: false });
    }
}
