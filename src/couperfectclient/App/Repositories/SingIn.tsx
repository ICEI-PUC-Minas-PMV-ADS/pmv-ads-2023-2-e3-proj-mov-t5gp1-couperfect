import api from './api';

interface SuccessResponse {
  token: string;
  name: string;
}

interface Request {
  email: string;
  plainTextPassword: string;
}

export async function signIn(request: Request) {
  try {
    const {data} = await api.post('api/players/singin', { email: request.email, plainTextPassword: request.plainTextPassword });
    return data;
  } catch (error) {
    console.log(error);
  }
}
