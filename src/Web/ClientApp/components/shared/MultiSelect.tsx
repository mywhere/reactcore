import * as React from "react";
import bind from 'bind-decorator';

import AppComponent from "@Components/shared/AppComponent";

export interface IProps extends React.DetailedHTMLProps<React.SelectHTMLAttributes<HTMLSelectElement>, HTMLSelectElement> {

}

export class MultiSelect extends AppComponent<IProps, {}> {
    protected elSelect: HTMLSelectElement;
    
    @bind
    getValues(): string[] {
        return Array.apply(null, this.elSelect.options).filter(x => x.selected).map(x => x.value);
    }

    render(): JSX.Element {
        return (
            <select ref={x => this.elSelect = x} key={this.renderKey} {...this.props} multiple={true}>
                {this.props.children}
            </select>
        );
    }
}