import * as React from "react";

export default class Footer extends React.PureComponent<{}> {
    render(): JSX.Element {
        return (
            <footer className="footer text-center">
                <p>Copyright (c) 2018 Nikolay Maev</p>
            </footer>
        );
    }
}