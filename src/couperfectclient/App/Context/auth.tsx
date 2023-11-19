import React, { ReactElement, createContext, useState } from 'react';
import * as SingInRepository from '../Repositories/SingIn';

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

interface AuthContextData {
  signed: boolean;
  user: object | null;
  signIn(request: AuthRequest): Promise<void>;
  signOut(): void;
}

interface AuthProviderProps extends React.PropsWithChildren {
  children: ReactElement;
}

interface AuthRequest{
  email: string;
  password: string;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [user, setUser] = useState<object | null>(null);

  async function signIn(request: AuthRequest) {
    const response = await SingInRepository.signIn({ email: request.email, plainTextPassword: request.password });
    
    console.log(response);
    
    setUser({  });
  }

  function signOut() {
    setUser(null);
  }

  return (
    <AuthContext.Provider  value={{signed: Boolean(user), user, signIn, signOut}}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
