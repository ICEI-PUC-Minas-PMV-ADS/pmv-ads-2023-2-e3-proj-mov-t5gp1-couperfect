import React, { ReactElement, createContext, useState } from "react";
import * as SingInRepository from '../Repositories/SingIn'

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

interface AuthContextData {
  signed: boolean;
  user: object | null;
  signIn(): Promise<void>;
}

interface AuthProviderProps extends React.PropsWithChildren {
  children: ReactElement
}

export const AuthProvider: React.FC<AuthProviderProps> = (props) => {
  const [user, setUser] = useState<object | null>(null);

  async function signIn() {
    const respose = await SingInRepository.signIn();

    setUser(respose.user);
  }

  return (
    <AuthContext.Provider  value={{signed: Boolean(user), user, signIn}}>
      {props.children}
    </AuthContext.Provider>
  )
};

export default AuthContext;