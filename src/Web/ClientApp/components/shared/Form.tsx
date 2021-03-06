import * as React from "react";
import { NSerializeJson } from "nserializejson";
import { NValTippy } from "nval-tippy";
import bind from 'bind-decorator';

import { emptyForm } from "@Utils";

export interface IProps extends React.DetailedHTMLProps<React.FormHTMLAttributes<HTMLFormElement>, HTMLFormElement> {
    children: any;
}

export class Form extends React.PureComponent<IProps> {
    public validator: NValTippy;

    private readonly _form: React.RefObject<HTMLFormElement>;

    constructor(props: IProps) {
        super(props);
        this._form = React.createRef();
    }

    @bind
    public isValid(): boolean {
        return this.validator.isValid();
    }

    @bind
    public emptyForm(): void {
        emptyForm(this._form.current);
    }

    @bind
    public getData<T>(): T {
        return NSerializeJson.serializeForm(this._form.current) as any as T;
    }

    componentDidMount() {
        this.validator = new NValTippy(this._form.current);
    }

    render(): JSX.Element {
        return (
            <form {...this.props} ref={this._form}>
                {this.props.children}
            </form>
        );
    }
}