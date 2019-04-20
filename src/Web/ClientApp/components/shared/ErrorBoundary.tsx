import * as React from "react";

interface ErrorBoundaryState {
    hasError: boolean;
}

export default class ErrorBoundary extends React.Component<{}, ErrorBoundaryState> {
    constructor(props) {
        super(props);
        this.state = { hasError: false };
    }

    componentDidCatch(error, info) {
        this.setState({ hasError: true });
        console.log(error, info);
    }

    render() {
        if (this.state.hasError) {
            return (<strong>Something went wrong.</strong>);
        }

        return this.props.children;
    }
}