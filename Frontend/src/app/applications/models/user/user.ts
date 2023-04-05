export interface UserModel {
  id: number;
  username: string;
  password: string;
  role: Role;
}

export interface Role {
  id: number;
  name: string;
  permissions: string[];
}
