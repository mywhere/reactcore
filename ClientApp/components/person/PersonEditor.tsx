import * as React from "react";
import bind from 'bind-decorator';
import { Formik } from 'formik';

import { IPersonModel } from "@Models/IPersonModel";
import { Form } from "@Components/shared/Form";

export interface IProps {
    data: IPersonModel;
}

export default class PersonEditor extends React.PureComponent<IProps> {
    public form: React.RefObject<any> = React.createRef();

    @bind
    public emptyForm(): void {
        if (this.form.current) {
            this.form.current.emptyForm();
        }
    }

    render(): JSX.Element {
        return (
            <Formik
                enableReinitialize={true}
                initialValues={{
                    firstName: this.props.data.firstName || '',
                    lastName: this.props.data.lastName || ''
                }}
                onSubmit={(_values) => {
                }}
            >
                {({
                    values,
                    handleChange,
                    handleBlur,
                    /* and other goodies */
                }) => (
                        <Form className="form" ref={this.form}>
                            <input type="hidden" name="id" defaultValue={(this.props.data.id || 0).toString()} />
                            <div className="form-group">
                                <label className="control-label required" htmlFor="person__firstName">First name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="person__firstName"
                                    name={nameof<IPersonModel>(x => x.firstName)}
                                    data-value-type="string"
                                    data-val-required="true"
                                    data-msg-required="First name is required."
                                    value={values.firstName}
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />
                            </div>
                            <div className="form-group">
                                <label className="control-label required" htmlFor="person__lastName">Last name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="person__lastName"
                                    name={nameof<IPersonModel>(x => x.lastName)}
                                    data-value-type="string"
                                    data-val-required="true"
                                    data-msg-required="Last name is required."
                                    value={values.lastName}
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                />
                            </div>
                        </Form>)}
            </Formik>
        );
    }
}