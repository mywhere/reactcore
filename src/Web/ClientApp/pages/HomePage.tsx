import * as React from "react";
import { RouteComponentProps } from "react-router";
import { Helmet } from "react-helmet";
import logo from "@Images/logo.png";

type Props = RouteComponentProps<{}>;

export default class HomePage extends React.PureComponent<Props> {
    render(): JSX.Element {
        return (
            <div>
                <Helmet>
                    <title>Home page - asp.net core with react</title>
                </Helmet>
                <img style={{"margin": "0 auto", "display": "block", "width": "100%"}} src={logo} />

                <p className="text-center" style={{"fontSize": "52px"}}>Happy coding!</p>
            </div>
        );
    }
}