import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { RedeSocial } from '@app/models/RedeSocial';
import { RedeSocialService } from '@app/services/redeSocial.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-redesSociais',
  templateUrl: './redesSociais.component.html',
  styleUrls: ['./redesSociais.component.scss'],
})
export class RedesSociaisComponent implements OnInit {
  modalRef = {} as BsModalRef;
  @Input() eventoId = 0;
  public formRS: FormGroup;
  public redeSocialAtual = { id: 0, nome: '', indice: 0 };

  opcoesRedesSociais = [
  { label: 'Youtube', value: 'fa-brands fa-youtube' },
  { label: 'Instagram', value: 'fa-brands fa-instagram' },
  { label: 'Facebook', value: 'fa-brands fa-facebook' },
  { label: 'X', value: 'fa-brands fa-x-twitter' },
  { label: 'Google', value: 'fa-brands fa-google' },
  { label: 'Linkedin', value: 'fa-brands fa-linkedin' },
  { label: 'Pinterest', value: 'fa-brands fa-pinterest' },
  { label: 'Whatsapp', value: 'fa-brands fa-whatsapp' },
  { label: 'Telegram', value: 'fa-brands fa-telegram' },
  { label: 'Skype', value: 'fa-brands fa-skype' },
  { label: 'Vimeo', value: 'fa-brands fa-vimeo' }
];

  public get redesSociais(): FormArray {
    return this.formRS.get('redesSociais') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private toaster: ToastrService,
    private redeSocialService: RedeSocialService
  ) {}

  ngOnInit() {
   this.carregarRedesSociais(this.eventoId);

    this.validation();
  }

  private carregarRedesSociais(id: number = 0): void {
    let origem = 'palestrante';
    if (this.eventoId !== 0) origem = 'evento';

    this.spinner.show();

    this.redeSocialService
      .getRedesSociais(origem, id)
      .subscribe(
        (redeSocialRetorno: RedeSocial[]) => {
          redeSocialRetorno.forEach((redeSocial) => {
            this.redesSociais.push(this.criarRedeSocial(redeSocial));
          });
        },
        (error: any) => {
          this.toaster.error('Erro ao tentar carregar redes sociais', 'Erro');
          console.error(error);
        }
      )
      .add(() => this.spinner.hide());
  }

  public validation(): void {
    this.formRS = this.fb.group({
      redesSociais: this.fb.array([]),
    });
  }

  adicionarRedeSocial(): void {
    this.redesSociais.push(this.criarRedeSocial({ id: 0 } as RedeSocial));
  }

  criarRedeSocial(redeSocial: RedeSocial): FormGroup {
    return this.fb.group({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required],
    });
  }

  public retornaTitulo(nome: string): string {
    return nome === null || nome === '' ? 'Rede Social' : nome;
  }

  public cssValidator(campoForm: FormControl | AbstractControl | null): any {
    return {
      'is-invalid': campoForm.errors && campoForm.touched,
    };
  }

  public salvarRedesSociais(): void {
    let origem = 'palestrante';
    if (this.eventoId !== 0) origem = 'evento';

    if (this.formRS.controls.redesSociais.valid) {
      this.spinner.show();
      this.redeSocialService
        .saveRedeSocial(origem, this.eventoId, this.formRS.value.redesSociais)
        .subscribe(
          () => {
            this.toaster.success(
              'Redes sociais salvas com sucesso.',
              'Sucesso!'
            );
          },
          (error: any) => {
            console.error(error);
            this.toaster.error('Erro ao salvar redes sociais', 'Erro!');
          }
        )
        .add(() => this.spinner.hide());
    }
  }

  public removerRedeSocial(template: TemplateRef<any>, indice: number): void {
    this.redeSocialAtual.id = this.redesSociais.get(indice + '.id').value;
    this.redeSocialAtual.nome = this.redesSociais.get(indice + '.nome').value;
    this.redeSocialAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });

  }

  confirmDeleteRedeSocial(): void {
    let origem = 'palestrante';
    this.modalRef.hide();
    this.spinner.show();

    if (this.eventoId !== 0) origem = 'evento';
    this.redeSocialService
      .deleteRedeSocial(origem, this.eventoId, this.redeSocialAtual.id)
      .subscribe(
        () => {
          this.toaster.success(
            'Rede social deletada com sucesso.',
            'Sucesso!'
          );
          this.redesSociais.removeAt(this.redeSocialAtual.indice);
        },
        (error: any) => {
          this.toaster.error(
            `Erro ao deletar rede social ${this.redeSocialAtual.id}`,
            'Erro!'
          );
          console.error(error);
        }
      )
      .add(() => this.spinner.hide());
  }
}
