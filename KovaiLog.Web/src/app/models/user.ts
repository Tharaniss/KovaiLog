export class User {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    token?: string;
}


export class UserToken {
    access_token: string;
    token_type: string;
    expires_in: string;
    userName: string;
    roles?: string;
}