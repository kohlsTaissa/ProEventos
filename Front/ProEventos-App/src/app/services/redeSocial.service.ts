import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RedeSocialService {
  baseURL = environment.apiURL + 'api/redesSociais';

  constructor(private http: HttpClient) {}

  /**
   * @param origem Precisa passa a palavra 'palestrante' ou 'evento' em minusculo
   * @param id precisa passar o palestranteId ou eventoId, dependendo da origem
   * @param redesSociais precisa adicionar redes sociais organizadas em RedeSocial[]
   * @returns
   */
  public getRedesSociais(origem: string, id: number): Observable<RedeSocial[]> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origem}`
        : `${this.baseURL}/${origem}/${id}`;

    return this.http.get<RedeSocial[]>(URL).pipe(take(1));
  }

  /**
   * @param origem Precisa passa a palavra 'palestrante' ou 'evento' em minusculo
   * @param id precisa passar o palestranteId ou eventoId, dependendo da origem
   * @param redesSociais precisa adicionar redes sociais organizadas em RedeSocial[]
   * @returns  retorna um observable de redes sociais
   */
  public saveRedeSocial(
    origem: string,
    id: number,
    redesSociais: RedeSocial[]
  ): Observable<RedeSocial[]> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origem}`
        : `${this.baseURL}/${origem}/${id}`;

    return this.http.put<RedeSocial[]>(URL, redesSociais).pipe(take(1));
  }

  /**
   * @param origem Precisa passa a palavra 'palestrante' ou 'evento' em minusculo
   * @param id precisa passar o palestranteId ou eventoId, dependendo da origem
   * @param redeSocialId precisa adicionar o id da rede social
   * @returns  retorna um observable any, pois é o retorno do controller
   */
  public deleteRedeSocial(
    origem: string,
    id: number,
    redeSocialId: number
  ): Observable<any> {
    let URL =
      id === 0
        ? `${this.baseURL}/${origem}/${redeSocialId}`
        : `${this.baseURL}/${origem}/${id}/${redeSocialId}`;

    return this.http.delete(URL).pipe(take(1));
  }
}
