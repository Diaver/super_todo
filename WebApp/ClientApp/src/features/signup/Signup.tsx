import React from 'react';
import {createStyles, makeStyles, Theme} from '@material-ui/core/styles';
import {Backdrop, Button, Container, Grid, Typography} from "@material-ui/core";
import {useDispatch, useSelector} from "react-redux";
import {TextValidator, ValidatorForm} from "react-material-ui-form-validator";

import {loadingSelector, loginAsync, signupslice, userSelector} from "./signupslice";
import CircularProgress from '@material-ui/core/CircularProgress';
import LockIcon from '@material-ui/icons/Lock';
import EmailIcon from '@material-ui/icons/Email';
import VpnKeyIcon from '@material-ui/icons/VpnKey';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        textBoxWithIconContainer: {
            display: "flex",
            alignItems: "end",
            marginTop: 32,
            "& div": {
                width: "100%"
            },
        },

        textBoxWithIcon: {
            flexGrow: 1,
            marginLeft: 8
        },
        form: {
            width: '100%', // Fix IE 11 issue.
        },
        backdrop: {
            zIndex: theme.zIndex.drawer + 1,
            color: '#fff',
        },
    }),
);

export function Signup() {

    const classes = useStyles();
    const dispatch = useDispatch();
    const user = useSelector(userSelector);
    const loading = useSelector(loadingSelector);

    return (
        <Container component="main" maxWidth="md">

            <Backdrop className={classes.backdrop} open={loading}>
                <CircularProgress color="inherit"/>
            </Backdrop>

            <ValidatorForm
                className={classes.form}
                onSubmit={() => {
                    dispatch(loginAsync());
                }}
                onError={errors => console.log(errors)}
            >
                <Grid
                    container
                    spacing={3}
                    direction="row"
                    justify="space-between"
                    alignItems="center">

                    <Grid item>
                        <Typography variant={"h4"}>
                            <LockIcon/> Login
                        </Typography>
                    </Grid>
                </Grid>


                <div className={classes.textBoxWithIconContainer}>
                    <EmailIcon/>
                    <TextValidator
                        variant="outlined"
                        name="Username"
                        className={classes.textBoxWithIcon}
                        fullWidth
                        label="Email"
                        placeholder="Please enter user's email"
                        helperText="email: admin@supertodo.com"
                        value={user.email}
                        onChange={(e: any) => dispatch(signupslice.actions.updateFieldValue({
                            field: "email",
                            value: e.target.value
                        }))}
                        validators={['required', 'isEmail']}
                        errorMessages={["This field is required", 'Email is not valid']}
                    />
                </div>
                <div className={classes.textBoxWithIconContainer}>
                    <EmailIcon/>
                    <TextValidator
                        variant="outlined"
                        name="Email"
                        className={classes.textBoxWithIcon}
                        fullWidth
                        label="Email"
                        placeholder="Please enter user's email"
                        helperText="email: admin@supertodo.com"
                        value={user.email}
                        onChange={(e: any) => dispatch(signupslice.actions.updateFieldValue({
                            field: "email",
                            value: e.target.value
                        }))}
                        validators={['required', 'isEmail']}
                        errorMessages={["This field is required", 'Email is not valid']}
                    />


                </div>

                <div className={classes.textBoxWithIconContainer}>
                    <VpnKeyIcon/>
                    <TextValidator
                        variant="outlined"
                        name="Password"
                        className={classes.textBoxWithIcon}
                        fullWidth={true}
                        label="Password"
                        type="password"
                        placeholder="Please enter Password (admin)"
                        helperText="password: admin"
                        value={user.password}
                        onChange={(e: any) => dispatch(signupslice.actions.updateFieldValue({
                            field: "password",
                            value: e.target.value
                        }))}

                        validators={['required']}
                        errorMessages={["This field is required"]}

                    />
                </div>
                <div className={classes.textBoxWithIconContainer}>
                    <Button startIcon={<LockIcon/>} type="submit" color="primary" fullWidth>Login</Button>
                </div>
            </ValidatorForm>
        </Container>
    );
}