import * as React from "react";
import { Helmet } from "react-helmet";
import { connect } from "react-redux";
import { Redirect, RouteComponentProps, withRouter } from "react-router";
import bind from 'bind-decorator';

import { ILoginModel } from "@Models/ILoginModel";
import Loader from "@Components/shared/Loader";
import { ApplicationState } from "@Store/index";
import { LoginStore } from "@Store/LoginStore";
import { Form as FormComponent } from "@Components/shared/Form";
import "@Styles/main.scss";

type Props = RouteComponentProps<{}> & typeof LoginStore.actionCreators & LoginStore.IState;

class LoginPage extends React.PureComponent<Props> {
    private readonly _loader: React.RefObject<Loader>;
    private _form: FormComponent;

    constructor(props: Props) {
        super(props);
        this._loader = React.createRef<Loader>();
    }

    componentDidMount() {
        this.props.init();

        if (this._loader) {
            this._loader.current.forceUpdate();
        }
    }

    @bind
    private async onClickSubmitBtn(e: React.MouseEvent<HTMLButtonElement>) {
        e.preventDefault();
        if (!!this._form && this._form.isValid()) {
            var data = this._form.getData<ILoginModel>();
            this.props.loginRequest(data);
        }
    }

    render(): JSX.Element {
        if (this.props.indicators.loginSuccess) {
            return (<Redirect to="/" />);
        }

        return (
            <div id="loginPage">
                <Helmet>
                    <title>Login page - asp.net core with react</title>
                </Helmet>

                <Loader ref={this._loader} show={this.props.indicators.operationLoading} />

                <div id="loginContainer">
                    <p className="text-center">Type any login and password to enter.</p>
                    <FormComponent ref={x => this._form = x }>
                        <div className="form-group">
                            <label htmlFor="inputLogin">Login</label>
                            <input type="text"
                                id="inputLogin"
                                name={nameof<ILoginModel>(x => x.login)}
                                data-value-type="string"
                                className="form-control"
                                data-val-required="true"
                                data-msg-required="Login is required." />
                        </div>
                        <div className="form-group">
                            <label htmlFor="inputLogin">Password</label>
                            <input type="password"
                                id="inputPassword"
                                name={nameof<ILoginModel>(x=>x.password)}
                                data-value-type="string"
                                className="form-control"
                                data-val-required="true"
                                data-msg-required="Password is required." />
                        </div>
                        <div className="form-inline">
                            <button className="btn btn-success" onClick={this.onClickSubmitBtn}>Sign in</button>
                        </div>
                    </FormComponent>
                </div>
            </div>
        );
    }
}

var component = connect(
    (state: ApplicationState) => state.login, // Selects which state properties are merged into the component's props
    LoginStore.actionCreators // Selects which action creators are merged into the component's props
)(LoginPage as any);

export default (withRouter(component as any) as any as typeof LoginPage)