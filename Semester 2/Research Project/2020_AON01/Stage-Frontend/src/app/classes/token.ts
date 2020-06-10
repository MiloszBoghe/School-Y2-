export class Token {
  constructor(
    public nameid: string,
    public sub: string,
    public jti: string,
    public email: string,
    public role: string,
    public exp: number,
    public iss: string,
    public aud: string,
    public voornaam: string,
    public naam: string,
    public isCoordinator = false
  ) {}
}
